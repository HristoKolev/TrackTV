import {Component, Input} from '@angular/core';
import {SimpleEpisode} from '../my-shows.models';

@Component({
    moduleId: module.id,
    selector: 'my-episode',
    templateUrl: 'my-episode.component.html',
})
export class MyEpisodeComponent {

    @Input()
    public episode : SimpleEpisode;
}