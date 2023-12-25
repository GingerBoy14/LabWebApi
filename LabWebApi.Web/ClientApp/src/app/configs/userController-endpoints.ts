import { environment } from 'src/environments/environment';

export const baseUrl = environment.apiUrl; 
export const userControllerUrl = '/User';
export const userProfileUrl = baseUrl + userControllerUrl + '/profile';
export const userProfileChangePasswordUrl = userProfileUrl + '/change-password';
export const uploadUserAvatarUrl = baseUrl + userControllerUrl + '/avatar';