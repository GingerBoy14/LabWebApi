import { environment } from 'src/environments/environment';

export const baseUrl = environment.apiUrl; 
export const productsControllerUrl = '/Product';
export const productsBaseUrl = baseUrl + productsControllerUrl;
export const productsUrl = baseUrl + productsControllerUrl + '/products';