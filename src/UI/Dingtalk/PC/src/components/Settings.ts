let backendHost;
let environment;

const location = window && window.location;

environment = "development";
backendHost = !location
    ? ""
    : location.protocol + "//" + location.hostname + ":5000";

const settings = {
    webApiUrl: `${backendHost}/api/`,
    environment
};

export default settings;
