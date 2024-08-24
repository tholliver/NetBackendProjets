export interface ICredentials {
    email: string
    password: string
}

export interface InfoCustomer {
    firstname: string
    lastname: string
    email: string
}

export interface IAuthToken {
    tokenType: string;
    accessToken: string;
    expiresIn: number;
    refreshToken: string;
}

export interface Customer {
    name: string
    lastname: string
    email: string
    password: string
}

export interface ErrorResponse {
    type: string;
    title: string;
    status: number;
    detail: string; // 'Failed' can be the default value, but keeping it as string allows for flexibility
}

export interface IZodError {
    code: string
    validation: string
    message: string
    path: string[]
}