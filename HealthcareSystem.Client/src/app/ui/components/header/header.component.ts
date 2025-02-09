import { Component } from '@angular/core';
import { MatGridList, MatGridTile } from '@angular/material/grid-list';
import { MatTab, MatTabGroup } from '@angular/material/tabs';

@Component({
  selector: 'app-header',
  imports: [MatTab, MatTabGroup],
  templateUrl: './header.component.html',
  styles: ``,
})
export class HeaderComponent {}
