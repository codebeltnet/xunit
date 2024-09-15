$version = minver -i
docfx metadata docfx.json
docker build -t yourbranding/classlibrary1:$version -f Dockerfile.docfx .
get-childItem -recurse -path api -include *.yml, .manifest | remove-item
