(function () {
    appModule.controller('common.views.users.editUserCarrystationModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.user', 'userId', 'userName',
        function ($scope, $uibModalInstance, userService, userId, userName) {
            var vm = this;

            vm.saving = false;
            vm.userName = userName;

            vm.save = function () {

                vm.saving = true;
                userService.setUserCarryStation({
                    userId: userId,
                    allCarrySets: vm.carryStations
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            vm.carryStations = [];

            function init() {
                userService.getUserCarryStations({ id: userId }).then(function (result) {
                   // console.log(result);
                    vm.carryStations = result.data;

                });

            }

            init();
        }
    ]);
})();