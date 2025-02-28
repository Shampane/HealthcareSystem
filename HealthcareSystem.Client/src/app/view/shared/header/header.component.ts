import { Component } from "@angular/core";
import { PagesLinks } from "../../interfaces/pagesLinks";
import { RouterLink } from "@angular/router";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrl: "header.component.scss",
  imports: [RouterLink],
})
export class HeaderComponent {
  links: PagesLinks[] = [
    { url: "", title: "home" },
    { url: "doctors", title: "doctors" },
    { url: "contact", title: "contact" },
    { url: "about", title: "about" },
  ];
}
