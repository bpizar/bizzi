import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { GlowMessages } from "../glowmessages/glowmessages.component";
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { SettingsService } from 'src/app/settings/settings.service';

@Component({
  selector: 'app-verify-tfa',
  templateUrl: './verify-tfa.component.template.html',
  styleUrls: ['./verify-tfa.component.css'],
  providers: [SettingsService]
})
export class VerifyTfaComponent implements OnInit {

  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('modalWindow') modalWindow: jqxWindowComponent;
  @ViewChild('secretInput') secretInput: ElementRef;
  @Output() onVerify = new EventEmitter<boolean>();

  secret: string = '';

  constructor(private settingsService: SettingsService) { }

  ngOnInit(): void {

  }

  showModal() {
    this.modalWindow.open();
  }

  Verify() {
    if (this.secret == '') {
      this.glowMessage.ShowGlow('error', 'Input Error', 'The Secret must not be empty!');
    }
    else {
      if (this.secret.length == 6) {
        this.settingsService.verifyTFAToken(this.secret)
          .subscribe(value => {
            if (value) {
              this.onVerify.emit(true);
              this.modalWindow.close();
            }
            else {
              this.glowMessage.ShowGlow('error', 'Verify Secret', 'Invalid Secret!');
            }
          });
      }
      else
        this.glowMessage.ShowGlow('error', 'Input Error', 'Invalid Secret!');
    }
    this.secretInput.nativeElement.focus();
  }

  cancel() {
    this.secret = '';
    this.onVerify.emit(false);
    this.modalWindow.close();
  }

  onKeyPress(evt) {
    if (!evt.key.match(/^[0-9._\b]+$/g)) {
      evt.preventDefault();
    }
  }

}
