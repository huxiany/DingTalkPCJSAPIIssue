class Settings {
    public static getWebApiUrl() {
        if (!window.location) {
            return "";
        }

        const loc = window.location;
        const webApiUrl = loc.protocol + "//" + loc.hostname + ":5000/api/";
        return webApiUrl;
    }
}

export default Settings;
