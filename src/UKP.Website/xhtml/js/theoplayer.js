Node.prototype.addClass = function(className) {
    this.classList.add(className);
    return this;
}

Node.prototype.addAttr = function(key, value) {
    this.setAttribute(key, value);
    return this;
}

Node.prototype.injectTooltip = function(tooltip) {
    return this.addAttr('title', tooltip);
}

Node.prototype.readTooltip = function() {
    return this.getAttribute('title');
}

function theoplayerOverrides() {}
theoplayerOverrides.prototype = {
    init: function(playerIn) {
        this.selectors = {
            container: document.getElementById('currentProgramDateTime'),
            buttons: {
                play: document.querySelector('.vjs-play-control'),
                quality: document.querySelector('.theoplayer-resolution-button'),
                mute: document.querySelector('.vjs-mute-control'),
                fullscreen: document.querySelector('.vjs-fullscreen-control')
            },
            player: playerIn
        };
        this.triggerEventFullscreen();
        this.triggerEventVolumechange();
    },
    playTime: function(date) {
        var timestring = ("0" + date.getHours()).slice(-2);
        timestring += ':';
        timestring += ("0" + date.getMinutes()).slice(-2);
        timestring += ':';
        timestring += ("0" + date.getSeconds()).slice(-2);

        var timeContainers = document.getElementsByClassName('vjs-current-time vjs-time-controls')[0].getElementsByClassName('vjs-current-time-display');
        if (timeContainers.length === 1) {
            var replacement = document.createElement('div').addClass('vjs-current-time-display').addClass('override-replacement').addAttr('aria-live', 'off');
            document.getElementsByClassName('vjs-current-time vjs-time-controls')[0].appendChild(replacement);
            timeContainers[0].addAttr('style', 'display: none;');
            timeContainers = document.getElementsByClassName('vjs-current-time-display');
        }

        timeContainers[1].textContent = timestring;
    },
    qualitySettings: function() {
        var that = this,
            qualitySettingNames = [
                'Very High',
                'High',
                'Medium',
                'Low',
                'Auto'
            ];

        try {
            var qualitySettings = document.querySelector('.theoplayer').querySelector('.theoplayer-resolution-button').querySelector('.vjs-menu-content').getElementsByTagName('li');
            if (typeof qualitySettings != 'undefined') {
                window.console && console.log(qualitySettings);

                if (qualitySettings.length == 0)
                    throw('Exception');

                iQualitySettingName = qualitySettingNames.length - qualitySettings.length;

                for (var i = 0; i < qualitySettings.length; i++) {
                    if (qualitySettings[i].innerText != 'auto') {
                        qualitySettings[i].innerText = qualitySettingNames[iQualitySettingName];
                        iQualitySettingName++;
                    }
                }

                return;
            }
            throw('Exception');
        }
        catch(e) {
            window.setTimeout(that.qualitySettings, 50);
        }
    },
    triggerEventFullscreen: function() {
        this.selectors.player.dispatchEvent(new Event('fullscreen'));
    },
    triggerEventVolumechange: function() {
        this.selectors.player.dispatchEvent(new Event('volumechange'));
    },
    triggerEventQualityChanged: function() {
        this.eventQualityChanged();
        this.selectors.player.dispatchEvent(new Event('qualityChanged'));
    },
    eventPlaying: function() {
        this.eventQualityChanged();
        this.selectors.buttons.play.injectTooltip('Pause clip');
    },
    eventPlay: function() {
        this.eventQualityChanged();
        this.selectors.buttons.play.injectTooltip('Pause clip');
    },
    eventPause: function() {
        this.eventQualityChanged();
        this.selectors.buttons.play.injectTooltip('Play clip');
    },
    eventTimeUpdate: function() {
        this.selectors.container.innerHTML = JSON.stringify(this.selectors.player.currentProgramDateTime);
    },
    eventVolumeChange: function() {
        var button = this.selectors.buttons.mute;

        if (button.classList.contains('vjs-vol-0'))
            button.injectTooltip('Unmute');
        
        else
            button.injectTooltip('Mute');
    },
    eventFullscreen: function() {
        var tip = 'Fullscreen';

        if (this.selectors.buttons.fullscreen.readTooltip() == 'Fullscreen')
            tip = 'Exit fullscreen';

        this.selectors.buttons.fullscreen.injectTooltip(tip);
    },
    eventQualityChanged: function() {
        this.qualitySettings();
        this.setQualityTooltip();
    },
    setQualityTooltip: function() {
        try {
            document.querySelector('.theoplayer-resolution-button').injectTooltip('Quality Settings');
        }
        catch(e) {
            window.setTimeout(this.setQualityTooltip, 50);
        }
    }
}

theoplayer = {
    configuration : {
        allowManualQualitySwitch : true
    },
    onReady: function () {
        var player = theoplayer.players[0],
            override = new theoplayerOverrides();

        player.addEventListener('pause',  override.eventPause.bind(override));
        player.addEventListener('play', override.eventPlay.bind(override));
        player.addEventListener('playing', override.eventPlaying.bind(override));
        player.addEventListener('timeupdate', override.eventTimeUpdate.bind(override));
        player.addEventListener('qualityChanged', override.eventQualityChanged.bind(override));
        player.addEventListener('volumechange', override.eventVolumeChange.bind(override));
        player.addEventListener('fullscreen', override.eventFullscreen.bind(override));

        override.init(player);
        override.selectors.buttons.fullscreen.addEventListener('mousedown', override.triggerFullscreen);

        player.play();
    }
}