import { CommentInfo } from './../models/comment/CommentDTO';
import { commentControllerUrl } from './../../configs/commentController-endpoints';
import { SimpleCommentInfo } from './../models/comment/SimpleCommentInfo';
import { Observable, of } from 'rxjs';

import { AlertService } from './Alert.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class CommentService {
  constructor(private http: HttpClient, private alertService: AlertService) {}
 
  createComment(comment: SimpleCommentInfo): Observable<CommentInfo> {
    return this.http.post<CommentInfo>(commentControllerUrl, comment).pipe(
      catchError((err) => {
        console.log(err())
        this.alertService.errorAlert(err().error.error, 'Create Comment Failed!');
        return of<CommentInfo>();
      })
    );
  }
 
}
