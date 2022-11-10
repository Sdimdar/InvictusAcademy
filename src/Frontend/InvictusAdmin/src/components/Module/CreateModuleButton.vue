<template>
    <q-btn color="primary" label="Создать модуль" @click="createModuleDialog = true" />
  
    <q-dialog v-model="createModuleDialog">
      <q-card style="min-width: 350px">
        <q-card-section>
          <div class="text-h6 text-center">Создать новый модуль</div>
        </q-card-section>
        <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
          <q-card-section class="q-pt-none">
  
            <q-input 
            dense 
            v-model="newModuleData.title" 
            label="Название" 
            lazy-rules
              :rules="[
                (val) => (val && val.length > 0) || 'Поле не должно быть пустым'
              ]"/>
  
            <q-input 
            dense 
            v-model="newModuleData.shortDescription" 
            label="Краткое описание" 
            lazy-rules
              :rules="[
                (val) => (val && val.length > 0) || 'Поле не должно быть пустым'
              ]"/>
  
          </q-card-section>
  
          <q-card-actions class="text-primary">
            <q-btn flat type="reset" label="Отмена" />
            <q-btn flat type="submit" label="Сохранить" />
          </q-card-actions>
        </q-form>
      </q-card>
    </q-dialog>
  </template>
    
  <script>
  import { defineComponent, ref } from "vue";
  import { fetchLoginedUserData, createModule } from "boot/axios";
  import notify from "boot/notifyes";
  
  export default defineComponent({
    name: "createModule-button",
    data() {
      return {
        data: [],
        newModuleData: {
            title: "",
            shortDescription:""
        },
        createModuleDialog: false
      };
    },
    methods: {
      async onSubmit() {
        try {
          const autorize = await fetchLoginedUserData();
          if (autorize.data.isSuccess) {
            const response = await createModule(this.newModuleData);
            if (response.data.isSuccess) {
            this.createModuleDialog = false;
            notify.showSucsessNotify("Модуль добавлен");
          }
          else {
            notify.showSucsessNotify("Пользователь не залогинен");
            response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          }
          }
          else{
            response.data.errors.forEach(error => {
              console.log(error)
            });
        }
        }
        catch (e) {
          notify.showErrorNotify(e.message);
      }
    },
      onReset() {
        this.newModuleData.title = "";
        this.newModuleData.shortDescription = "";
        this.createModuleDialog = false;
        this.errorMessage = "";
      }
    },
  });
  </script>
    