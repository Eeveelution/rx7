<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateUsersTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('users', function (Blueprint $table) {
            $table->id();
            $table->string('username');
            $table->string('email')->unique();
            $table->timestamp('email_verified_at')->nullable();
            $table->string('password');

            $table->string("location")->default("");
            $table->string("occupation")->default("");
            $table->string("website")->default("");
            $table->string("twitter")->default("");
            $table->string("discord")->default("");
            //Standard Stats
            $table->bigInteger("standard_ranked_score")->default(0);
            $table->bigInteger("standard_total_score")->default(0);
            $table->double    ("standard_level")->default(0);
            $table->integer   ("standard_playcount")->default(0);
            $table->double    ("standard_accuracy")->default(0);
            //Taiko Stats
            $table->bigInteger("taiko_ranked_score")->default(0);
            $table->bigInteger("taiko_total_score")->default(0);
            $table->double    ("taiko_level")->default(0);
            $table->integer   ("taiko_playcount")->default(0);
            $table->double    ("taiko_accuracy")->default(0);
            //CTB Stats
            $table->bigInteger("catch_ranked_score")->default(0);
            $table->bigInteger("catch_total_score")->default(0);
            $table->double    ("catch_level")->default(0);
            $table->integer   ("catch_playcount")->default(0);
            $table->double    ("catch_accuracy")->default(0);

            //Standard grades
            $table->integer("standard_count_ssh")->default(0);
            $table->integer("standard_count_ss")->default(0);
            $table->integer("standard_count_sh")->default(0);
            $table->integer("standard_count_s")->default(0);
            $table->integer("standard_count_a")->default(0);
            $table->integer("standard_count_b")->default(0);
            $table->integer("standard_count_c")->default(0);
            $table->integer("standard_count_d")->default(0);
            //Taiko grades
            $table->integer("taiko_count_ssh")->default(0);
            $table->integer("taiko_count_ss")->default(0);
            $table->integer("taiko_count_sh")->default(0);
            $table->integer("taiko_count_s")->default(0);
            $table->integer("taiko_count_a")->default(0);
            $table->integer("taiko_count_b")->default(0);
            $table->integer("taiko_count_c")->default(0);
            $table->integer("taiko_count_d")->default(0);
            //CTB grades
            $table->integer("catch_count_ssh")->default(0);
            $table->integer("catch_count_ss")->default(0);
            $table->integer("catch_count_sh")->default(0);
            $table->integer("catch_count_s")->default(0);
            $table->integer("catch_count_a")->default(0);
            $table->integer("catch_count_b")->default(0);
            $table->integer("catch_count_c")->default(0);
            $table->integer("catch_count_d")->default(0);

            //Standard Accuracy
            $table->integer("standard_acc300")->default(0);
            $table->integer("standard_acc100")->default(0);
            $table->integer("standard_acc50")->default(0);
            $table->integer("standard_accgeki")->default(0);
            $table->integer("standard_acckatu")->default(0);
            $table->integer("standard_accmiss")->default(0);
            //Taiko Accuracy
            $table->integer("taiko_acc300")->default(0);
            $table->integer("taiko_acc100")->default(0);
            $table->integer("taiko_acc50")->default(0);
            $table->integer("taiko_accgeki")->default(0);
            $table->integer("taiko_acckatu")->default(0);
            $table->integer("taiko_accmiss")->default(0);
            //CTB Accuracy
            $table->integer("catch_acc300")->default(0);
            $table->integer("catch_acc100")->default(0);
            $table->integer("catch_acc50")->default(0);
            $table->integer("catch_accgeki")->default(0);
            $table->integer("catch_acckatu")->default(0);
            $table->integer("catch_accmiss")->default(0);

            $table->integer("replays_watched")->default(0);
            $table->tinyInteger("priviledges")->default(0);

            $table->timestamp("created_at")->useCurrent();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('users');
    }
}
