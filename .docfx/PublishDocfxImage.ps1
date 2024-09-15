$version = minver -i
docker tag sharedkernel-docfx:$version yourbranding/classlibrary1:$version
docker push yourbranding/classlibrary1:$version
