<template>
  <q-btn label="Создать комнату" @click="showRoomModal = false" />
  <div class="q-pa-md q-gutter-sm">
    <q-dialog v-model="showRoomModal">
      <q-card class="modal-dialog">
        <q-card-section>
          <div class="text-h6">Создание комнаты</div>
        </q-card-section>
        <q-form @submit="">
          <q-card-section class="q-pt-none">
            <q-input v-model="roomModel.name" label="Название урока" />
          </q-card-section>

          <q-card-actions align="right" class="text-primary">
            <q-btn flat label="Отменить" v-close-popup />
            <q-btn type="submit" flat label="Создать"/>
          </q-card-actions>
        </q-form>
      </q-card>
    </q-dialog>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'AddLesson',
  setup () {
    return {
      showRoomModal: ref(false)
    }
  },
  data () {
    return {
      roomModel: {
        name: ''
      }
    }
  },
  methods: {
    // async addRoom () {
    //   const response = (await addRoom(this.roomModel))
    //   if (response.status === 200) {
    //     this.showRoomModal = false
    //   }
    // }
  }
}
</script>

<style scoped>
.modal-dialog{
  width: 300px;
}
</style>
