import {
    compare,
    different,
    password,
    required,
} from '@rxweb/reactive-form-validators';

export class ProfilePassword {
    @required()
    @password({
      validation: { minLength: 5, digit: true, specialCharacter: true },
    })
    currentPassword: any;
    @required()
    @password({
      validation: { minLength: 5, digit: true, specialCharacter: true },
    })
    @different({ fieldName: 'currentPassword' ,})
    newPassword: any;
  }
  