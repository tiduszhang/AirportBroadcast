(function () {
    appModule.controller('common.views.audioSets.topPwrPort.createOrEdit', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioTopPwrPort', 'entityId',
        function ($scope, $uibModalInstance, fileUploader, audioTopPwrPort, entityId) {
            var vm = this;
          
            vm.saving = false;
            vm.save = function () {
                vm.saving = true; 
                if (entityId) {
                    //修改
                    audioTopPwrPort.update(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                } else {
                    //创建CreateArtcleCatalog
                    audioTopPwrPort.create(vm.entity).then(function () {
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
                    audioTopPwrPort.get({
                        id: entityId
                    }).then(function (result) {
                        vm.entity = result.data; 
                        console.log(vm.entity);
                    });                     
                }

            }

           
            init();
        }
    ]);
})();