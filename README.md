# dotnet core publish

```
dotnet publish PuppeteerSharpDotNetCodeInDockerDemo.csproj -c Release
```



# docker build

```
docker build --tag chadyeh/simple-docker-demo:v1.0.0 .
```



# docker run

```
docker run --security-opt=seccomp:unconfined -it chadyeh/simple-docker-demo:v1.0.0
```