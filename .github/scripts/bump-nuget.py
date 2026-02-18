#!/usr/bin/env python3
"""
Bumps PackageVersion entries in Directory.Packages.props.
- For packages published by the triggering source repo: uses the known TRIGGER_VERSION.
- For all other packages: queries NuGet API for latest stable version.
- Preserves all XML formatting via regex (no ElementTree rewriting).
"""

import re, os, urllib.request, json, sys
from typing import Dict, List

TRIGGER_SOURCE = os.environ.get("TRIGGER_SOURCE", "")
TRIGGER_VERSION = os.environ.get("TRIGGER_VERSION", "")

# Maps source repo name → NuGet package ID prefixes published by that repo.
# Keep this aligned with what each repo actually publishes.
SOURCE_PACKAGE_MAP: Dict[str, List[str]] = {
    "cuemon": [
        "Cuemon.",
    ],
    "xunit": [
        "Codebelt.Extensions.Xunit",
    ],
    "benchmarkdotnet": [
        "Codebelt.Extensions.BenchmarkDotNet",
    ],
    "bootstrapper": [
        "Codebelt.Bootstrapper",
    ],
    "newtonsoft-json": [
        "Codebelt.Extensions.Newtonsoft.Json",
        "Codebelt.Extensions.AspNetCore.Mvc.Formatters.Newtonsoft",
    ],
    "aws-signature-v4": [
        "Codebelt.Extensions.AspNetCore.Authentication.AwsSignature",
    ],
    "unitify": [
        "Codebelt.Unitify",
    ],
    "yamldotnet": [
        "Codebelt.Extensions.YamlDotNet",
        "Codebelt.Extensions.AspNetCore.Mvc.Formatters.Text.Yaml",
    ],
    "globalization": [
        "Codebelt.Extensions.Globalization",
    ],
    "asp-versioning": [
        "Codebelt.Extensions.Asp.Versioning",
    ],
    "swashbuckle-aspnetcore": [
        "Codebelt.Extensions.Swashbuckle",
    ],
    "savvyio": [
        "Savvyio.",
    ],
    "shared-kernel": [],
}


def nuget_latest(pkg: str) -> str | None:
    """Query NuGet flat container API for the latest stable version."""
    try:
        url = f"https://api.nuget.org/v3-flatcontainer/{pkg.lower()}/index.json"
        with urllib.request.urlopen(url, timeout=15) as r:
            versions = json.loads(r.read())["versions"]
        stable = [v for v in versions if not re.search(r"-", v)]
        return stable[-1] if stable else None
    except Exception as e:
        print(f"  SKIP {pkg}: {e}", file=sys.stderr)
        return None


def triggered_version(pkg: str) -> str | None:
    """Return TRIGGER_VERSION if pkg is published by the triggering source repo."""
    if not TRIGGER_VERSION or not TRIGGER_SOURCE:
        return None
    prefixes = SOURCE_PACKAGE_MAP.get(TRIGGER_SOURCE, [])
    if any(pkg.startswith(p) for p in prefixes):
        return TRIGGER_VERSION
    return None


with open("Directory.Packages.props", "r") as f:
    content = f.read()

changes = []


def replace_version(m: re.Match) -> str:
    pkg = m.group(1)
    current = m.group(2)

    new_ver = triggered_version(pkg) or nuget_latest(pkg) or current

    if new_ver != current:
        changes.append(f"  {pkg}: {current} → {new_ver}")
    return m.group(0).replace(f'Version="{current}"', f'Version="{new_ver}"')


pattern = re.compile(
    r'<PackageVersion\b'
    r'(?=[^>]*\bInclude="([^"]+)")'
    r'(?=[^>]*\bVersion="([^"]+)")'
    r'[^>]*>',
    re.DOTALL,
)
new_content = pattern.sub(replace_version, content)

if changes:
    print(f"Bumped {len(changes)} package(s):")
    print("\n".join(changes))
else:
    print("All packages already at target versions.")

with open("Directory.Packages.props", "w") as f:
    f.write(new_content)
