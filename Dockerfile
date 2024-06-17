# 基础阶段
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 构建镜像
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# 构建配置参数：通过 ARG 指定构建配置（默认为 Release）
ARG BUILD_CONFOGURATION=Release
WORKDIR /src
# 复制项目文件：将项目文件复制到容器中，确保它们的相对路径保持不变
COPY ["FormulaOne.Api/FormulaOne.Api.csproj", "FormulaOne.Api/"]
COPY ["FormulaOne.DataService/FormulaOne.DataService.csproj", "FormulaOne.DataService/"]
COPY ["FormulaOne.Entities/FormulaOne.Entities.csproj", "FormulaOne.Entities/"]
# 还原依赖项
RUN dotnet restore "FormulaOne.Api/FormulaOne.Api.csproj"
# 复制所有文件
COPY . .
# 设置工作目录
WORKDIR "/src/FormulaOne.Api"
# 构建项目 并将输出放在 /app/build 目录中
RUN dotnet build "FormulaOne.Api.csproj" -c $BUILD_CONFOGURATION -o /app/build

# 发布镜像
FROM build AS publish
ARG BUILD_CONFOGURATION=Release
# 发布项目：运行 dotnet publish
RUN dotnet publish "FormulaOne.Api.csproj" -c $BUILD_CONFOGURATION -o /app/publish

# 最终镜像
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# 入口点：定义容器启动时运行的命令，这里是 dotnet FormulaOne.Api.dll，用于启动 ASP.NET Core 应用程序
ENTRYPOINT [ "dotnet", "FormulaOne.Api.dll" ]