import { Component, Input, input } from "@angular/core";

@Component({
  selector: "app-modal-card",
  imports: [],
  templateUrl: "./modal-card.component.html",
  styleUrl: "./modal-card.component.scss",
})
export class ModalCardComponent {
  title = input.required<string>();
  messages = input.required<string[]>();
  isSuccess = input.required<boolean>();

  buttonLabel = input.required<string>();
  @Input({ required: true }) buttonFunction: () => void = (): void => console.log();

  onButtonClick() {
    this.buttonFunction();
  }
}
