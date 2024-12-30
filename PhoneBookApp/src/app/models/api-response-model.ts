export class APIResponseModel<T> {
    isError: boolean;
    message: string;
    data: T
}