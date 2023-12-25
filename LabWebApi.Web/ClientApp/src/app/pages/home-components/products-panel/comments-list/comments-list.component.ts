import { AlertService } from './../../../../core/services/Alert.service';
import { SimpleCommentInfo } from './../../../../core/models/comment/SimpleCommentInfo';
import { CommentService } from './../../../../core/services/Comment.service';
import { commentControllerUrl } from './../../../../configs/commentController-endpoints';
import { ProductService } from './../../../../core/services/Product.service';
import { CommentInfo } from './../../../../core/models/comment/CommentDTO';
import { Component, Input, OnInit } from '@angular/core';
@Component({
  selector: 'comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.css'],
})
export class CommentsListComponent implements OnInit {
  comments: CommentInfo[];
  @Input()
  productId: string;
  newCommentText: string;
  constructor(
    private productService: ProductService,
    private commentService: CommentService,
    private alertService: AlertService,
  ) {}
  async createComment() {
    const newCommentData: SimpleCommentInfo = {
      text: this.newCommentText,
      productId: this.productId,
      publicationDate: new Date(),
    };

    this.commentService
      .createComment(newCommentData)
      .subscribe((data: CommentInfo) => {
        this.alertService.successAlert('Successful', 'Create comment');

        this.comments = [...this.comments, data];
        this.newCommentText = ''
      });
  }
  async ngOnInit() {
    this.productService
      .getProductComments(this.productId)
      .subscribe((comments: CommentInfo[]) => {
        console.log(comments);
        this.comments = comments;
      });
  }
}
