class Util {
    public getCacheOption(
        key: string,
        durationInMin: number
    ): { type: string; key: string; duration: number } {
        return {
            type: "localStorage", // 缓存方式, 默认为'localStorage'
            key, // !!! 唯一必选的参数, 用于内部存储 !!!
            duration: 1000 * 60 * durationInMin // 缓存的有效期长, 以毫秒数指定
        };
    }

    public getSessionCacheOption(
        key: string,
        durationInMin: number
    ): { type: string; key: string; duration?: number } {
        return {
            type: "sessionStorage",
            key
        };
    }
}

export default new Util();
