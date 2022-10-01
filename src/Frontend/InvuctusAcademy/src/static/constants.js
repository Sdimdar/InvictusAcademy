const EMAIL_REGEXP = /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/iu;

const PWD_REGEXP = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;

const loginConfig = {
    headers: {
        "Content-Type": "application/json",
    },
    withCredentials: true
};

export default {EMAIL_REGEXP, PWD_REGEXP, loginConfig}