import {
  alpha,
  email,
  minLength,
  required,
} from '@rxweb/reactive-form-validators';

export class SimpleUserInfo {
  @required() id: any;
  @required()
  @minLength({ value: 5 })
  @alpha()
  name: string;
  @required()
  @minLength({ value: 5 })
  @alpha()
  surname: string;
  @required() @email() email: string;
}
