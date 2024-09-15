$version = minver -i -t v -v w
docker tag xunit-docfx:$version jcr.codebelt.net/geekle/xunit-docfx:$version
docker push jcr.codebelt.net/geekle/xunit-docfx:$version
