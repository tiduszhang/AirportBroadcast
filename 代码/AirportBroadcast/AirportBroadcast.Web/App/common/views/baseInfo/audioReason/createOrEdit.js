(function () {
    appModule.controller('common.views.baseinfo.audioReason.createOrEdit', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioReason',
        'entityId', 'abp.services.app.commonLookup',
        function ($scope, $uibModalInstance, fileUploader, audioReason, entityId, commonLookup) {
            var vm = this;
            vm.languages = [];
            vm.saving = false;
            vm.save = function () {
                vm.saving = true;
                 
                if (entityId) {
                    //修改
                    audioReason.update(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                } else {
                    //创建CreateArtcleCatalog
                    audioReason.create(vm.entity).then(function () {
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
                    audioReason.get({
                        id: entityId
                    }).then(function (result) {
                        vm.entity = result.data;  
                    });                     
                }

                commonLookup.getAudioLanguageForCombobox().then(function (result) {
                    vm.languages = result.data.items;                     
                });

            }
             

            init();
        }
    ]);
})();