type FieldValue = string | number


export interface IZodError {
    code: string
    validation: string
    message: string
    path: string[]
}