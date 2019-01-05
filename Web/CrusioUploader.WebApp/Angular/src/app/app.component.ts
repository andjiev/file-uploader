import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { IFileDto } from './models/models';
import { ApiService } from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  uploader: FileUploader;
  files: IFileDto[];

  constructor(private apiService: ApiService) {
    this.uploader = new FileUploader({ url: `${environment.apiUrl}/api/files` });

    this.uploader.onBeforeUploadItem = (item) => {
      item.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item: any, response: string) => {
      var file = JSON.parse(response) as IFileDto;
      this.files.unshift(file);
    }
  }

  ngOnInit() {
    this.apiService.getFiles().subscribe(
      response => {
        this.files = response;
      });
  }

  public uploadFile(): void {
    this.uploader.uploadAll();
  }

  public downloadFile(fileUid: string): void {
    window.location.href = `${environment.apiUrl}/api/files/${fileUid}`;
  }
}
