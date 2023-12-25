import { SimpleCommentInfo } from './SimpleCommentInfo';
import { SimpleProductInfo } from './../products/SimpleProductInfo';
import { propObject, required } from '@rxweb/reactive-form-validators';
import { SimpleUserInfo } from './../admin/SimpleUserInfo';

export class CommentInfo extends SimpleCommentInfo {
    id: string;
    @required() userWhoCreatedId: string
    @required()  @propObject(SimpleProductInfo) product: SimpleProductInfo;
    @required() @propObject(SimpleUserInfo) userWhoCreated: SimpleUserInfo;
}