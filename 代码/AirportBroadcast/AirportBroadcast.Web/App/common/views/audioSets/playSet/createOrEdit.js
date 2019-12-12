(function () {
    appModule.controller('common.views.audioSets.playSet.createOrEdit', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioPlaySet', 'entityId',
        function ($scope, $uibModalInstance, fileUploader, audioPlaySet, entityId) {
            var vm = this;
          
            vm.saving = false;
            vm.save = function () {
                vm.saving = true; 
                if (entityId) {
                    //修改
                    audioPlaySet.update(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                } else {
                    //创建CreateArtcleCatalog
                    audioPlaySet.create(vm.entity).then(function () {
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
                    audioPlaySet.get({
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