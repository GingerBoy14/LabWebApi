import {
    minLength,
    required,
    numeric
} from '@rxweb/reactive-form-validators';

export class SimpleProductInfo {
 id: string;
    @required() @minLength({ value: 5 }) name: string;
    @required() description: string;
    @required()
    @numeric() price: number;
  publicationDate: Date;
  }