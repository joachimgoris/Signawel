export const BASE_URL = "https://localhost:5001/api";
export const AUTHENTICATE_URL = BASE_URL + "/authentication";
export const AUTHENTICATE_LOGIN_URL = AUTHENTICATE_URL + "/login";
export const AUTHENTICATE_REFRESH_URL = AUTHENTICATE_URL + "/refresh";
export const    AUTHENTICATE_CONFIRMEMAIL_URL = AUTHENTICATE_URL + "/confirmemail";
export const IMAGES = BASE_URL + "/images";
export const DETERMINATION_GRAPH = BASE_URL + "/determination-graph";
export const ROADWORK_SCHEMAS = BASE_URL + "/roadwork-schemas";
export const REPORT_GROUPS = BASE_URL + "/reportgroups";
export const CITIES = REPORT_GROUPS + "/cities";
export const PRIORITY_EMAILS = BASE_URL + "/priority-emails";
export const REPORTS = BASE_URL + "/reports"

// GIPOD
export const GIPOD_BASE_URL = 'http://api.gipod.vlaanderen.be/ws/v1';
export const ROADWORK_INFO = GIPOD_BASE_URL + '/WorkAssignment?Id=';