import {
    required
} from '@rxweb/reactive-form-validators';


export class SimpleCommentInfo {
    @required() text:string
    @required() productId:string
    @required() publicationDate: Date;
  }