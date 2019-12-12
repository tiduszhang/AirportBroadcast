(function () {
    appModule.controller('common.views.baseinfo.audioGate.createOrEdit', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioGate',
        'entityId', 'abp.services.app.commonLookup',
        function ($scope, $uibModalInstance, fileUploader, audioGate, entityId, commonLookup) {
            var vm = this;
            vm.languages = [];
            vm.saving = false;
            vm.save = function () {
                vm.saving = true;
                 
                if (entityId) {
                    //修改
                    audioGate.update(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                } else {
                    //创建CreateArtcleCatalog
                    audioGate.create(vm.entity).then(function () {
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
                    audioGate.get({
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