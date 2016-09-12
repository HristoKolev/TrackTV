import {Component, Input} from '@angular/core';
import {SimpleEpisode} from '../my-shows.models';

@Component({
    moduleId: module.id,
    selector: 'my-episode-component',
    templateUrl: 'my-episode.component.html',
})
export class MyEpisodeComponent {

    @Input()
    private episode : SimpleEpisode;
}