const path = require("path");
const CleanWebpackPlugin = require("clean-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");

const config = {
    entry: ["./src/app/app.tsx"],
    output: {
        path: path.resolve(__dirname, "dist"),
        filename: "[name].bundle.js",
        chunkFilename: "[name].bundle.js",
        publicPath: "/"
    },
    optimization: {
        runtimeChunk: "single",
        splitChunks: {
            cacheGroups: {
                vendors: {
                    test: /[\\/]node_modules[\\/]/,
                    name: "vendors",
                    chunks: "all"
                },
                components: {
                    test: /components[\\/]/,
                    name: "components",
                    chunks: "all"
                }
            }
        }
    },
    module: {
        rules: [
            { test: /\.js$/, exclude: /node_modules/, use: "babel-loader" },
            { test: /\.(png|jpg|gif)$/, use: "file-loader" },
            {
                test: /\.tsx?$/,
                use: [
                    {
                        loader: "babel-loader"
                    },
                    {
                        loader: "ts-loader",
                        options: {
                            transpileOnly: true
                        }
                    }
                ]
            }
        ]
    },
    plugins: [
        new HtmlWebpackPlugin({
            title: "JSAPI Test",
            template: "html/index.html",
            appMountId: "App"
        }),
        new CleanWebpackPlugin(["dist"])
    ],
    resolve: {
        extensions: [".ts", ".tsx", ".js", ".jsx"]
    }
};

module.exports = config;
