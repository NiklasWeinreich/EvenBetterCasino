import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RofusComponent } from './rofus.component';

describe('RofusComponent', () => {
  let component: RofusComponent;
  let fixture: ComponentFixture<RofusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RofusComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RofusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
