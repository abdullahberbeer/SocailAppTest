import { Component, Input, OnInit } from '@angular/core';
import { Image } from '../_models/model/image.model';

@Component({
  selector: 'photo-gallery',
  templateUrl: './photo-gallery.component.html',
  styleUrls: ['./photo-gallery.component.css']
})
export class PhotoGalleryComponent implements OnInit {
@Input() images?:Image[];
  constructor() { }

  ngOnInit(): void {
  }

}
