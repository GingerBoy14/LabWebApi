import {
    alpha,
    email,
    minLength,
    required,
} from '@rxweb/reactive-form-validators';

export class UserProfile {
    @required() id: any;
    @required()
    @minLength({ value: 5 })
    @alpha()
    name: string;
    @required()
    @minLength({ value: 5 })
    @alpha()
    surname: string;
    @required() @minLength({ value: 5 }) userName: string;
    @required() @email() email: string;
    @required() birthday: Date;
  }
  