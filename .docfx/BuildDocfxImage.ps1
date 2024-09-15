$version = minver -i -t v -v w
docfx metadata docfx.json
docker buildx build -t xunit-docfx:$version --platform linux/arm64,linux/amd64 --load -f Dockerfile.docfx .
get-childItem -recurse -path api -include *.yml, .manifest | remove-item
