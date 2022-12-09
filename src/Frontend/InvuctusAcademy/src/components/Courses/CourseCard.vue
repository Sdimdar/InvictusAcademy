<template>
    <div>
      <q-card class="my-card">
        <q-img src="https://cdn.quasar.dev/img/parallax2.jpg">
          <div class="absolute-bottom">
            <div class="text-h6">{{ data.name }}</div>
          </div>
        </q-img>

        <q-card-actions>
          <q-btn v-if="data.purchased" v-bind:href="'/user/ShowFullCourseModules/' + data.id" flat color="primary">
                    Перейти к курсу
                </q-btn>
                <q-btn v-if="!data.purchased" v-bind:href="'/user/ShowCourseModules/' + data.id" flat color="primary">
                    Купить
                </q-btn>
                <q-btn v-if="!data.purchased" @click="openPage(data.id)">
                    Детали
                </q-btn>
        </q-card-actions>
    </q-card>

    </div>
</template>

<script>
export default {
    props: {
        data: Object
    },
    methods:{
    openPage(id){
      console.log(id);
      this.$router.push({ path: '/user/courseDetails', query: { id: id } })
    },
  }
}
</script>

<style lang="scss" scoped>
    .course-img{
        height: 100px;
        width: 500px;
    }

    .my-card{
      margin: 10px;
      width: 220px;
}
</style>
