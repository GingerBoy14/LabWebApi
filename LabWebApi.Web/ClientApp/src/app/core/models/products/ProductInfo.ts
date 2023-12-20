import {
    propObject,
    required
} from '@rxweb/reactive-form-validators';

import { SimpleProductInfo } from './SimpleProductInfo';
import { SimpleUserInfo } from '../admin/SimpleUserInfo';

export class ProductInfo extends SimpleProductInfo {
    @required() @propObject(SimpleUserInfo) userWhoCreated: SimpleUserInfo;
  }