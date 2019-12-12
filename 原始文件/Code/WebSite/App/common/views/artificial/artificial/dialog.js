(function () {
    appModule.controller('common.views.artificial.artificial.dialog', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.receiveJson', 'abp.services.app.audioPlaySet',
        function ($scope, $uibModalInstance, fileUploader, receiveJson, audioPlaySet) {
            var vm = this;
            vm.languages = [];
            vm.saving = false;
           

            vm.save = function () {
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
                    //  $uibModalInstance.close();
                }, error => {
                    abp.notify.error("播放失败");
                    vm.saving = false;
                }).finally(function () {
                    vm.saving = false;
                });

            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();

            };

            function init() {
                audioPlaySet.getDeviceList({
                    id: 0
                }).then(function (result) {
                    vm.deviceList = result.data;
                });

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

            init();

        }
    ]);
})();