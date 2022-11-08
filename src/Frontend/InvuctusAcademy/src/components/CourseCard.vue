<template>
    <div>
        <q-card :class="$attrs.class" class="my-card" flat bordered>
            <q-card-section horizontal class="flex justify-between">
                <q-card-section class="q-pt-xs">
                    <div class="text-h5 q-mt-sm q-mb-xs">{{ data.name }}</div>
                    <div class="text-caption text-grey">
                        {{ data.description }}
                    </div>
                </q-card-section>

                <q-card-section class="col-5 flex flex-center">
                    <q-img class="rounded-borders course-img" src="https://cdn.quasar.dev/img/parallax2.jpg" />
                </q-card-section>
            </q-card-section>

            <q-separator />

            <q-card-actions class="flex justify-end">
                <q-btn v-if="data.purchased" v-bind:href="'/user/ShowCourseModules?id=' + data.id" flat color="primary">
                    Перейти к курсу
                </q-btn>
                <q-btn v-if="!data.purchased" v-bind:href="'/user/ShowCourseModules?id=' + data.id" flat color="primary">
                    Купить
                    
                </q-btn>
                
                

            </q-card-actions>
        </q-card>
    </div>
</template>

<script>
import { route } from 'quasar/wrappers'
export default {
    props: {
        data: Object
    }
}
</script>

<style lang="scss" scoped>
    .course-img{
        height: 100px;
        width: 500px;
    }
</style>