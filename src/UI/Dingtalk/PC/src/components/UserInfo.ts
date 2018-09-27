import nattyStorage from "natty-storage";
import Util from "./Util";

class UserInfo {
    public static loggedIn(): boolean {
        const cache = this.getStorage();
        const result = cache.has(this.cacheKey);
        return result.has;
    }

    public static setUserInfo(userInfo: IUserInfo): void {
        if (!userInfo) {
            return;
        }
        const cache = this.getStorage();
        cache.set(this.cacheKey, userInfo);
    }

    public static getDDConfig(): any {
        const cache = this.getStorage();
        const ddConfig = cache.get(this.ddConfigCacheKey);
        return ddConfig;
    }

    public static setDDConfig(ddConfig): void {
        if (!ddConfig) {
            return;
        }
        const cache = this.getStorage();
        cache.set(this.ddConfigCacheKey, ddConfig);
    }

    private static storage: any = null;
    private static storageKey: string = "userInfoStorage";
    private static cacheKey: string = "userInfo";
    private static ddConfigCacheKey: string = "ddConfig";
    private static cacheDurationInMin: number = 10;

    private static getStorage(): any {
        if (this.storage === null) {
            this.storage = nattyStorage(
                Util.getSessionCacheOption(
                    this.storageKey,
                    this.cacheDurationInMin
                )
            );
        }
        return this.storage;
    }
}
export default UserInfo;
