(function () {
    appModule.controller('common.views.baseinfo.audioLanguage.createOrEdit', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioLanguage', 'entityId',
        function ($scope, $uibModalInstance, fileUploader, audioLanguage, entityId) {
            var vm = this;
          
            vm.saving = false;
            vm.save = function () {
                vm.saving = true; 
                if (entityId) {
                    //修改
                    audioLanguage.update(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                } else {
                    //创建CreateArtcleCatalog
                    audioLanguage.create(vm.entity).then(function () {
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
                    audioLanguage.get({
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