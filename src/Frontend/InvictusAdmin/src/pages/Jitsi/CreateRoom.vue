<template>
  <div class="q-pa-md" style="max-width: 300px">
    <q-input
      ref="inputRef"
      filled
      v-model="streamingRoom.name"
      label="Название комнаты"
      :rules="[ myRule ]"
    />
  </div>
  <div class="q-pa-md" style="max-width: 300px">
    <q-input
      ref="inputRef"
      filled
      v-model="streamingRoom.imageLink"
      label="Адрес картинки для комнаты"
      :rules="[ myRule ]"
    />
  </div>
  <q-btn color="black" label="Создать комнату" @click="submitRoom" />
</template>

<script>
import {ref} from "vue";
import {createStreamingRoom} from "boot/axios";
import notify from "boot/notifyes";
import router from "src/router";

export default {
name: "CreateRoom",
  data(){
    return{
      streamingRoom:{
        name: "",
        imageLink: ""
      }
    }
  },
  setup () {
    const inputRef = ref(null)

    return {
      searchModels: ref(''),
      options: [
        'true', 'false'
      ],
      // model: ref(''),
      inputRef,
      // price: ref(null),
      // text: ref(''),

      myRule(val) {
        // simulating a delay

        return new Promise((resolve, reject) => {
          setTimeout(() => {
            // call
            //  resolve(true)
            //     --> content is valid
            //  resolve(false)
            //     --> content is NOT valid, no error message
            //  resolve(error_message)
            //     --> content is NOT valid, we have error message
            resolve(!!val || '* Required')

            // calling reject(...) will also mark the input
            // as having an error, but there will not be any
            // error message displayed below the input
            // (only in browser console)
          }, 1000)
        })
      }
    }
  },
  methods:{
    async submitRoom() {
      try {
        console.log(this.streamingRoom)
        const response = await createStreamingRoom(this.streamingRoom);
        console.log(response)
        if (response.data.isSuccess) {
          console.log(response.data.value)
          notify.showSucsessNotify("Комната создана");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    }
  }
}
</script>

<style scoped>

</style>
