(function () {
    appModule.controller('common.views.audioSets.device.createOrEdit', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioDevice', 'entityId',
        function ($scope, $uibModalInstance, fileUploader, audioDevice, entityId) {
            var vm = this;
          
            vm.saving = false;
            vm.save = function () {
                vm.saving = true; 
                if (entityId) {
                    //修改
                    audioDevice.update(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                } else {
                    //创建CreateArtcleCatalog
                    audioDevice.create(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                }
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();

            };


            function init() {
                if (entityId) {
                    audioDevice.get({
                        id: entityId
                    }).then(function (result) {
                        vm.entity = result.data; 
                        console.log(vm.entity);
                    });                     
                }
                //初始化实际声卡内容
                audioDevice.getVoiceDevice().then(function (result) {
                    vm.voiceDevice = result.data;
                    console.log(vm.voiceDevice);
                });
            }

           
            init();
        }
    ]);
})();