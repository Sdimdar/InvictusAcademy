<template>
  <q-page-container>

    <div class="row justify-content-start">

      <div class="col-lg-4 col-md-12 col-sm-12 article-img-box"
           v-for="room in this.allRooms"
      >
        <div class="card" @click="$router.push(`/streamingRooms/room/${room.address}`)">
          <div class="card-body">
            <h5 class="card-title">{{room.name}}</h5>
          </div>
          <img class="card-img-top" :src="`${room.imageLink}`"
               alt="Card image cap"
          >
        </div>
      </div>


    </div>

    <div class="q-pa-lg flex flex-center">
      <q-pagination
        v-model="this.pageNumber"
        @click="getAllStreamingRooms"
        color="black"
        :max="countPages"
        :max-pages="10"
        :boundary-numbers="false"
      />
    </div>
  </q-page-container>
</template>

<script>
import {ref} from "vue";
import {getAllStreamingRooms, getCountStreamingRooms} from "boot/axios";

export default {
  name: "allRooms",
  setup () {
    return {
      current: ref(5)
    }
  },
  data(){
    return{
      allRooms: [{}],
      pageNumber: 1,
      countPages: 0,
      pageSize: 5,
    }
  },
  mounted() {
    this.getAllStreamingRooms();
  },
  methods:{
    async getAllStreamingRooms() {
      try {
        const response = await getAllStreamingRooms(this.pageNumber, this.pageSize);
        if (response.data.isSuccess) {
          this.allRooms = response.data.value.streamingRooms;
          await this.getCountStreamingRooms();
        } else {
          response.data.errors.forEach(element => {
            notify.showErrorNotify(element);
          });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    async getCountStreamingRooms() {
      try {
        const response = await getCountStreamingRooms();
        if (response.data.isSuccess) {
          this.countPages = Math.ceil(response.data.value / this.pageSize)
          console.log(response.data)
        } else {
          response.data.errors.forEach(element => {
            notify.showErrorNotify(element);
          });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    }
  }
}
</script>

<style scoped>

.article-img-box {
  margin-bottom: 100px;
  height: 160px;
  width: 270px;
}

.card-title{
  font-size: medium;
  text-align: center;
}

.card-body{
  padding-bottom: 1px;
  padding-top: 1px;
}
</style>
