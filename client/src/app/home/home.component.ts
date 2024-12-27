import { Component } from '@angular/core';
import { IGallary } from '../shared/models/gallary';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  descriptionPath : string = "index.html";
  projectDescription : string = "";
  images: IGallary[] = [];

  constructor(private http: HttpClient)
  {
      this.http
          .get('assets/indexBuilder/' + this.descriptionPath,
              { responseType: 'text' })
          .subscribe(data => {
              this.projectDescription = data;
          }
      );
  }


  ngOnInit(): void {
    this.getGallaries();
  }
  getGallaries() {
    this.images.push({
      id:1,
      url:"assets/images/hero1.jpg"
    });
    this.images.push({
      id:2,
      url:"assets/images/hero2.jpg"
    });
    this.images.push({
      id:3,
      url:"assets/images/hero3.jpg"
    });
    this.images.push({
      id:4,
      url:"assets/images/hero4.jpg"
    });
    this.images.push({
      id:5,
      url:"assets/images/hero5.jpg"
    });
    this.images.push({
      id:6,
      url:"assets/images/hero6.jpg"
    });
  }

}
