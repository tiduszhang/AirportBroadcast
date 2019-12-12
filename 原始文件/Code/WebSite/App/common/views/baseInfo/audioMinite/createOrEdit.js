(function () {
    appModule.controller('common.views.baseinfo.audioMinite.createOrEdit', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioMinite',
        'entityId', 'abp.services.app.commonLookup',
        function ($scope, $uibModalInstance, fileUploader, audioMinite, entityId, commonLookup) {
            var vm = this;
            vm.languages = [];
            vm.saving = false;
            vm.save = function () {
                vm.saving = true;
                 
                if (entityId) {
                    //修改
                    audioMinite.update(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                } else {
                    //创建CreateArtcleCatalog
                    audioMinite.create(vm.entity).then(function () {
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
                    audioMinite.get({
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