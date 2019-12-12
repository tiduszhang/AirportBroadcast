(function () {
    //设备管理
    appModule.controller('common.views.artificial.handArtificial.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.audioPlaySet',
        'abp.services.app.receiveJson',
        function ($scope, $uibModal, $stateParams, uiGridConstants, audioPlaySet, receiveJson) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            
            vm.saving = false;
            vm.languages = [];
            vm.entity = {
                topPortIds: [],
                playText: "",
                playTimes:2
            };


            
            vm.HandPlay = function () {
                vm.saving = true;

                vm.entity.topPortIds = [];
                for (var i = 0; i < vm.topPwrPortList.length; i++) {
                    if (vm.topPwrPortList[i].isChoose == true) {
                        vm.entity.topPortIds.push(vm.topPwrPortList[i].id);
                    }
                }
                //创建CreateArtcleCatalog
                receiveJson.handPlayText(vm.entity).then(function () {
                    abp.notify.info("播放成功"); 
                }, error => {
                    abp.notify.error("播放失败");
                    vm.saving = false;
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.init = function() {
                //audioPlaySet.getDeviceList({
                //    id: 0
                //}).then(function (result) {
                //    vm.deviceList = result.data;
                //});
                vm.entity = {
                    topPortIds: [],
                    playText: "",
                    playTimes: 2
                };

                audioPlaySet.getTopPwrPortList({
                    dType: 3,
                    id: 0
                }).then(function (result) {
                    vm.topPwrPortList = result.data;
                    for (var i = 0; i < vm.topPwrPortList.length; i++) {
                        vm.topPwrPortList[i].isChoose = true;
                    }
                });
            }

            vm.init();
             
        }]);
})();