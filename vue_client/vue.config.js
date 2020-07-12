module.exports = {
    //前端server和接口代理
    devServer: {
        host: 'localhost',
        port: 8080,
        proxy: {
            '/api/*': {
                target: 'http://localhost:5000'
            }
        }
    },
    //项目的基本路径
    publicPath: '/',
    //项目的生成目录
    outputDir: 'dist',
    //修改index.html的路径
    indexPath: 'index.html',
    //为静态资源设置hash值，这样重复请求资源都会从服务器获取新的数据 而不是读取缓存数据
    filenameHashing: true,
    //eslint是否开启
    lintOnSave: false,
    //是否启用sourceMap
    productionSourceMap: false
}