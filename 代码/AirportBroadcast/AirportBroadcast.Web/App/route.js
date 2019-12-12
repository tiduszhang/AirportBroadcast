angular.module("app-route", [])
.config([
    '$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
         //*************硬件设置

        if (abp.auth.hasPermission('Pages.AudioSets.Device')) {
            $stateProvider.state('audioSetsDevice', {
                url: '/audioSetsDevice',
                templateUrl: '~/App/common/views/audioSets/device/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.AudioSets.TopPwrPort')) {
            $stateProvider.state('audioSetsTopPwrPort', {
                url: '/audioSetsTopPwrPort',
                templateUrl: '~/App/common/views/audioSets/topPwrPort/index.cshtml'
            });
        }


         //end*************硬件设置

        //*************基本语音文件
        if (abp.auth.hasPermission('Pages.BaseInfo.AudioAirLine')) {
            $stateProvider.state('audioAirLine', {
                url: '/audioAirLine',
                templateUrl: '~/App/common/views/baseInfo/audioAirLine/index.cshtml'
            });
        }
        if (abp.auth.hasPermission('Pages.BaseInfo.AudioCheckIn')) {
            $stateProvider.state('audioCheckIn', {
                url: '/audioCheckIn',
                templateUrl: '~/App/common/views/baseInfo/audioCheckIn/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.BaseInfo.AudioCity')) {
            $stateProvider.state('audioCity', {
                url: '/audioCity',
                templateUrl: '~/App/common/views/baseInfo/audioCity/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.BaseInfo.AudioConst')) {
            $stateProvider.state('audioConst', {
                url: '/audioConst',
                templateUrl: '~/App/common/views/baseInfo/audioConst/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.BaseInfo.AudioDigit')) {
            $stateProvider.state('audioDigit', {
                url: '/audioDigit',
                templateUrl: '~/App/common/views/baseInfo/audioDigit/index.cshtml'
            });
        }
       
        if (abp.auth.hasPermission('Pages.BaseInfo.AudioGate')) {
            $stateProvider.state('audioGate', {
                url: '/audioGate',
                templateUrl: '~/App/common/views/baseInfo/audioGate/index.cshtml'
            });     
        }

        if (abp.auth.hasPermission('Pages.BaseInfo.AudioHour')) {
            $stateProvider.state('audioHour', {
                url: '/audioHour',
                templateUrl: '~/App/common/views/baseInfo/audioHour/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.BaseInfo.AudioLanguage')) {
            $stateProvider.state('audioLanguage', {
                url: '/audioLanguage',
                templateUrl: '~/App/common/views/baseInfo/audioLanguage/index.cshtml'
            });     
        }

        if (abp.auth.hasPermission('Pages.BaseInfo.AudioMinite')) {            
            $stateProvider.state('audioMinite', {
                url: '/audioMinite',
                templateUrl: '~/App/common/views/baseInfo/audioMinite/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.BaseInfo.AudioReason')) {
            $stateProvider.state('audioReason', {
                url: '/audioReason',
                templateUrl: '~/App/common/views/baseInfo/audioReason/index.cshtml'
            });
        }
                
        if (abp.auth.hasPermission('Pages.BaseInfo.AudioTurnPlate')) {
            $stateProvider.state('audioTurnPlate', {
                url: '/audioTurnPlate',
                templateUrl: '~/App/common/views/baseInfo/audioTurnPlate/index.cshtml'
            });
        }

         //end*************基本语音文件

         //*************基本信息

        if (abp.auth.hasPermission('Pages.BaseInfo.PlaySet')) {
            $stateProvider.state('playSet', {
                url: '/playSet',
                templateUrl: '~/App/common/views/audioSets/playSet/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.BaseInfo.PlaySet.Global')) {
            $stateProvider.state('playSetGlobal', {
                url: '/playSetGlobal',
                templateUrl: '~/App/common/views/audioSets/playSetGlobal/index.cshtml'
            });
        }
       
        if (abp.auth.hasPermission('Pages.BaseInfo.AudioTemplte')) {
            $stateProvider.state('audioTemplte', {
                url: '/audioTemplte',
                templateUrl: '~/App/common/views/baseInfo/audioTemplte/index.cshtml'
            });
        }

         
        if (abp.auth.hasPermission('Pages.Logs.ReciveLogs')) {
            $stateProvider.state('reciveLogs', {
                url: '/reciveLogs',
                templateUrl: '~/App/common/views/logs/reciveLogs/index.cshtml'
            });

        }

        if (abp.auth.hasPermission('Pages.Logs.PlayLogs')) {
            $stateProvider.state('playLogs', {
                url: '/playLogs',
                templateUrl: '~/App/common/views/logs/playLogs/index.cshtml'             
            });

        }


        if (abp.auth.hasPermission('Pages.Artificial')) {
            $stateProvider.state('artificial', {
                url: '/artificial',
                templateUrl: '~/App/common/views/artificial/artificial/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.HandArtificial')) {
            $stateProvider.state('handartificial', {
                url: '/handartificial',
                templateUrl: '~/App/common/views/artificial/handArtificial/index.cshtml'
            });
        }

        /*

        if (abp.auth.hasPermission('Pages.Play.BroadList')) {
            $stateProvider.state('broadlist', {
                url: '/broadlist',
                templateUrl: '~/App/common/views/play/broadlist/index.cshtml'
            });
            $stateProvider.state('broadlist.arr', {
                url: '/broadlistArr',
                views: {
                    'myhz-views': {
                        templateUrl: '~/App/common/views/play/broadlist/arr/arr.cshtml'
                    }
                }
            });
            $stateProvider.state('broadlist.dep', {
                url: '/broadlistDep',
                views: {
                    'myhz-views': {
                        templateUrl: '~/App/common/views/play/broadlist/dep/dep.cshtml'
                    }
                }
            });
            $stateProvider.state('broadlist.recivelogs', {
                url: '/broadlistRecivelogs',
                views: {
                    'myhz-views': {
                        templateUrl: '~/App/common/views/play/broadlist/recivelogs.cshtml'
                    }
                }
            });
            $stateProvider.state('broadlist.playlog', {
                url: '/broadlistPlaylog',
                views: {
                    'myhz-views': {
                        templateUrl: '~/App/common/views/play/broadlist/playlog.cshtml'
                    }
                }
            });

        }

*/
         
    }]);