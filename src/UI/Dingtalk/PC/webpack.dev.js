const merge = require("webpack-merge");
const webpack = require("webpack");
const common = require("./webpack.common.js");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = merge(common, {
    mode: "development",
    devtool: "inline-source-map",
    devServer: {
        contentBase: "./dist",
        host: "0.0.0.0",
        port: 3001,
        hot: true,
        open: false
    },
    module: {
        rules: [{ test: /\.css$/, use: ["style-loader", "css-loader"] }]
    },
    plugins: [
        new webpack.HotModuleReplacementPlugin(),
        new MiniCssExtractPlugin({
            filename: "[name].css",
            chunkFilename: "[name].css"
        })
    ]
});
